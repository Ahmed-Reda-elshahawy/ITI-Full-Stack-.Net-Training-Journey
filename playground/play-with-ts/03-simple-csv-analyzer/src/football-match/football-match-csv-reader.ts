import { CsvReader } from '../utils/csv-reader';
import { FootballMatch, MatchResult } from './football-match';

export class FootballMatchCsvReader extends CsvReader<FootballMatch> {
  protected override map(dataRow: string[]): FootballMatch {
    return {
      date: new Date(dataRow[0]),
      homeTeam: dataRow[1],
      awayTeam: dataRow[2],
      homeTeamScore: parseInt(dataRow[3]),
      awayTeamScore: parseInt(dataRow[4]),
      matchResult: dataRow[5] as MatchResult,
      referee: dataRow[6],
    };
  }
}
