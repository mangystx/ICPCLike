import { defineStore } from 'pinia'
import axios from 'axios'

export interface Person {
    id: number
    name: string
    sex: string | null
    dateOfBirth: string
    role: string
}

export interface SeasonSolved {
    seasonName: string
    totalSolved: number
}

export interface TeamResultsCount {
    teamName: string
    resultsCount: number
}

export interface NameCount {
    name: string
    count: number
}

export interface ParticipantResult {
    participant: string
    team: string
    stage: string
    rank: number
}

export interface ParticipantWithTeam {
    person: Person
    teamName: string | null
}

export interface TeamAverageSolved {
    teamName: string
    avgSolved: number
}

export interface TeamHighPenalty {
    teamName: string
    totalPenalty: number
}

export interface TeamWithOrg {
    teamName: string
    orgName: string
}

export enum Role {
    Contestant = 0,
    Reserve = 1,
    Coach = 2
}

export const roleLabels: Record<Role, string> = {
    [Role.Contestant]: 'Учасник',
    [Role.Reserve]: 'Запасний',
    [Role.Coach]: 'Тренер'
}

const API_BASE_URL = 'https://localhost:7279/api/statistics'
import { queryConfigs } from '@/config/query-configs'

export const useStatisticsStore = defineStore('statistics', {
    state: () => ({
        participants: [] as Person[],
        loading: false as boolean,
        error: null as string | null,
        additionalResult: [] as any[],
        countries: [] as string[]
    }),
    actions: {
        async fetchParticipants() {
            this.loading = true
            this.error = null

            try {
                const response = await axios.get<Person[]>(`${API_BASE_URL}/participants`)
                this.participants = response.data
            } catch (err: any) {
                this.error = err.message ?? 'Unknown error'
            } finally {
                this.loading = false
            }
        },

        async fetchCountries() {
            try {
                const res = await axios.get<string[]>(`${API_BASE_URL}/organizations/countries`)
                this.countries = res.data
            } catch (e) {
                console.error('Помилка при завантаженні країн:', e)
                this.countries = []
            }
        },

        async runSelectedQuery(key: string, input: Record<string, any>) {
            this.additionalResult = []
            try {
                const config = queryConfigs[key]
                const urlBase = `${API_BASE_URL}/${config.path}`

                if (config.method === 'GET') {
                    const query = config.params?.map(p => `${p}=${encodeURIComponent(input[p])}`).join('&') ?? ''
                    const url = query ? `${urlBase}?${query}` : urlBase
                    const res = await axios.get(url)
                    this.additionalResult = res.data
                } else if (config.method === 'POST') {
                    const body: Record<string, any> = {}
                    for (const field of config.bodyFields ?? []) {
                        body[field] = input[field]
                    }
                    const res = await axios.post(urlBase, body)
                    this.additionalResult = res.data
                }
            } catch (e: any) {
                console.error(e)
                this.additionalResult = []
            }
        }
    }
})