export type QueryConfig = {
	label: string
	method: 'GET' | 'POST'
	path: string
	params?: string[]
	bodyFields?: string[]
}

export const queryConfigs: Record<string, QueryConfig> = {
	// getAllParticipants: {
	// 	label: "Усі учасники",
	// 	method: 'GET',
	// 	path: 'participants'
	// },
	getParticipantsBornBetween: {
		label: "Учасники з діапазоном дати народження",
		method: 'GET',
		path: 'participants/between',
		params: ['from', 'to']
	},
	getParticipantsByCountries: {
		label: "Учасники з вказаних країн",
		method: 'POST',
		path: 'participants/by-countries',
		bodyFields: ['countries']
	},
	searchParticipantsByName: {
		label: "Пошук учасників за імʼям",
		method: 'GET',
		path: 'participants/search',
		params: ['pattern']
	},
	getFemaleCoaches: {
		label: "Жінки-тренери",
		method: 'GET',
		path: 'participants/female-coaches'
	},
	getMaleOrReserve: {
		label: "Чоловіки або запасні",
		method: 'GET',
		path: 'participants/male-or-reserve'
	},
	getDistinctRoles: {
		label: "Унікальні ролі",
		method: 'GET',
		path: 'roles/distinct'
	},
	getOldestParticipant: {
		label: "Найстарший учасник",
		method: 'GET',
		path: 'participants/oldest'
	},
	getAverageSolvedTasks: {
		label: "Середня кількість розвʼязаних задач",
		method: 'GET',
		path: 'results/average-solved-tasks'
	},
	getStageCount: {
		label: "Кількість етапів",
		method: 'GET',
		path: 'stages/count'
	},
	getAvgSolvedTasksPerSeason: {
		label: "Розвʼязані задачі по сезонах",
		method: 'GET',
		path: 'results/solved-per-season'
	},
	getVisibleTeamStats: {
		label: "Кількість результатів для публічних команд",
		method: 'GET',
		path: 'teams/visible-results'
	},
	getParticipantsWithMinRank: {
		label: "Учасники з мінімальним рангом",
		method: 'GET',
		path: 'participants/min-rank'
	},
	getJoinedResults: {
		label: "Кількість участей кожного учасника",
		method: 'GET',
		path: 'participants/joined-count'
	},
	getParticipantResults: {
		label: "Участі учасників з командами і етапами",
		method: 'GET',
		path: 'participants/results'
	},
	getLeftJoinedParticipants: {
		label: "Учасники + команди (left join)",
		method: 'GET',
		path: 'participants/left-joined'
	},
	getParticipantsWithRightJoin: {
		label: "Учасники без команди (right join)",
		method: 'GET',
		path: 'participants/right-joined'
	},
	getParticipantsInStage: {
		label: "Учасники в першому етапі",
		method: 'GET',
		path: 'participants/in-stage'
	},
	getParticipantsInTeams: {
		label: "Пошук учасників у командах",
		method: 'GET',
		path: 'participants-in-teams',
		params: ['pattern']
	},
	getTeamAverageSolved: {
		label: "Середня кількість задач на команду",
		method: 'GET',
		path: 'teams/average-solved'
	},
	getTeamsWithHighPenalty: {
		label: "Команди з великим штрафом",
		method: 'GET',
		path: 'teams/high-penalty'
	},
	getParticipantsWithMostResults: {
		label: "Учасники з найбільшою кількістю участей",
		method: 'GET',
		path: 'participants-most-results'
	},
	getParticipantsWithAboveAvgResults: {
		label: "Учасники з результатом вище середнього",
		method: 'GET',
		path: 'participants-above-avg'
	},
	getParticipantsWithResultsExist: {
		label: "Учасники з принаймні однією участю",
		method: 'GET',
		path: 'participants-exist'
	},
	getParticipantsWithAnyParticipation: {
		label: "Учасники з участями в командах з результатами",
		method: 'GET',
		path: 'participants-any'
	},
	getParticipantsInOrganizations: {
		label: "Учасники з організацій України",
		method: 'GET',
		path: 'participants-orgs'
	},
	getTeamWithOrgFromSubquery: {
		label: "Команди з організаціями (Польща)",
		method: 'GET',
		path: 'teams-orgs'
	},
	getTeamsWithSubstitutions: {
		label: "Команди із замінами в одному сезоні",
		method: 'GET',
		path: 'teams/with-substitutions'
	}
}