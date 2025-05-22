<template>
    <div class="table-wrapper">
        <h1>Учасники</h1>
        <div v-if="loading">Завантаження...</div>
        <div v-if="error">Помилка: {{ error }}</div>
        <div v-if="!loading && !error" class="table-scroll">
            <table>
                <thead>
                    <tr>
                        <th>#</th>
                        <th @click="sortBy('name')">Ім'я <span class="inline-block w-[1ch]">{{ getSortLabel('name')
                        }}</span></th>
                        <th @click="sortBy('sex')">Стать <span class="inline-block w-[1ch]">{{ getSortLabel('sex')
                        }}</span></th>
                        <th @click="sortBy('dateOfBirth')">Дата народження <span class="inline-block w-[1ch]">{{
                            getSortLabel('dateOfBirth') }}</span></th>
                        <th @click="sortBy('role')">Роль <span class="inline-block w-[1ch]">{{ getSortLabel('role')
                        }}</span></th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="(p, index) in sortedParticipants" :key="p.id">
                        <td>{{ index + 1 }}</td>
                        <td>{{ p.name }}</td>
                        <td>{{ p.sex }}</td>
                        <td>{{ formatDate(p.dateOfBirth) }}</td>
                        <td>{{ roleLabels[p.role] ?? p.role }}</td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="query-selector">
            <label for="querySelector">Оберіть запит:</label>
            <div class="query-controls">
                <select id="querySelector" v-model="selectedQuery">
                    <option v-for="(cfg, key) in queryConfigs" :key="key" :value="key">
                        {{ cfg.label }}
                    </option>
                </select>
            </div>
        </div>
        <div class="query-inputs" v-if="inputFields.length">
            <div v-for="field in inputFields" :key="field" class="field">
                <label :for="field">{{ field }}:</label>
                <select v-if="field === 'countries'" v-model="inputValues[field]" :id="field" multiple>
                    <option v-for="country in store.countries" :key="country" :value="country">
                        {{ country }}
                    </option>
                </select>
                <input v-else v-model="inputValues[field]" :id="field" type="text" />
            </div>
        </div>
        <div class="query-controls" style="margin-top: 1rem">
            <button @click="runSelectedQuery">Виконати запит</button>
        </div>
        <div v-if="selectedQuery === 'getDistinctRoles' && additionalResult && additionalResult.length"
            class="query-result">
            <h2>Унікальні ролі:</h2>
            <ul>
                <li v-for="r in additionalResult" :key="r">
                    {{ roleLabels[r] ?? `Невідомо (${r})` }}
                </li>
            </ul>
        </div>

        <div v-else-if="numericResultQueries.includes(selectedQuery) && additionalResult !== undefined && additionalResult !== null"
            class="query-result">
            <h2>Результат запиту:</h2>
            <p class="text-lg font-semibold">{{ additionalResult }}</p>
        </div>
        <div v-else-if="objectResultQueries.includes(selectedQuery) && additionalResult && typeof additionalResult === 'object'"
            class="query-result">
            <h2>Результат запиту:</h2>
            <pre>{{ JSON.stringify(additionalResult, null, 2) }}</pre>
        </div>
        <div v-else-if="Array.isArray(additionalResult) && additionalResult.length" class="query-result">
            <h2>Результат запиту:</h2>
            <table class="dynamic-table">
                <thead>
                    <tr>
                        <th v-for="(val, key) in additionalResult[0]" :key="key">{{ formatHeader(key) }}</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="(row, idx) in additionalResult" :key="idx">
                        <td v-for="(val, key) in row" :key="key">
                            <template v-if="typeof val === 'object' && val !== null">
                                <table class="nested-table">
                                    <tr v-for="(v, k) in val" :key="k">
                                        <th>{{ k }}</th>
                                        <td>{{ formatValue(v) }}</td>
                                    </tr>
                                </table>
                            </template>
                            <template v-else>
                                {{ formatValue(val) }}
                            </template>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>

    </div>
</template>

<script setup lang="ts">
import { onMounted, ref, computed } from 'vue'
import { useStatisticsStore, roleLabels } from '@/stores/statistics'
import { queryConfigs } from '@/config/query-configs'
import { watch } from 'vue'

const store = useStatisticsStore()
const sortKey = ref<'name' | 'sex' | 'dateOfBirth' | 'role' | null>(null)
const sortDirection = ref<'asc' | 'desc'>('asc')
const selectedQuery = ref<string>('getDistinctRoles')
const inputValues = ref<Record<string, any>>({})
const numericResultQueries = ['getAverageSolvedTasks', 'getStageCount']
const objectResultQueries = ['getOldestParticipant']

onMounted(async () => {
    await store.fetchParticipants()
    await store.fetchCountries()
})

watch(selectedQuery, () => {
    store.additionalResult = []
    inputValues.value = {}
})

const sortedParticipants = computed(() => {
    if (!sortKey.value) return store.participants
    return [...store.participants].sort((a, b) => {
        const aVal = a[sortKey.value!]
        const bVal = b[sortKey.value!]
        if (aVal < bVal) return sortDirection.value === 'asc' ? -1 : 1
        if (aVal > bVal) return sortDirection.value === 'asc' ? 1 : -1
        return 0
    })
})

function sortBy(key: typeof sortKey.value) {
    if (sortKey.value === key) {
        sortDirection.value = sortDirection.value === 'asc' ? 'desc' : 'asc'
    } else {
        sortKey.value = key
        sortDirection.value = 'asc'
    }
}

function getSortLabel(key: typeof sortKey.value) {
    if (sortKey.value !== key) return ''
    return sortDirection.value === 'asc' ? '↑' : '↓'
}

function formatDate(date: string) {
    return new Date(date).toLocaleDateString('uk-UA')
}

const inputFields = computed(() => {
    const cfg = queryConfigs[selectedQuery.value]
    return cfg ? [...(cfg.params ?? []), ...(cfg.bodyFields ?? [])] : []
})

function runSelectedQuery() {
    store.runSelectedQuery(selectedQuery.value, inputValues.value)
}

function formatValue(val: unknown) {
    if (val instanceof Date) return val.toLocaleDateString('uk-UA')
    if (typeof val === 'string' && /^\d{4}-\d{2}-\d{2}/.test(val)) return formatDate(val)
    return val
}

function formatHeader(key: string) {
    return key
        .replace(/([A-Z])/g, ' $1')
        .replace(/^./, str => str.toUpperCase())
}

const loading = computed(() => store.loading)
const error = computed(() => store.error)
const additionalResult = computed(() => store.additionalResult)
</script>
