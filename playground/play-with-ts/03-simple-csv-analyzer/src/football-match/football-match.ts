export enum MatchResult {
  Home = 'H',
  Away = 'W',
  Draw = 'D',
  None = 'N/A',
}

export interface FootballMatch {
  date: Date;
  homeTeam: string;
  awayTeam: string;
  homeTeamScore: number;
  awayTeamScore: number;
  matchResult: MatchResult;
  referee: string;
}
