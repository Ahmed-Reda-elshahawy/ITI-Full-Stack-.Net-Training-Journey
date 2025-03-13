import { FootballMatch } from '../football-match/football-match';
import { FootballMatchCsvReader } from './football-match-csv-reader';

export class FootballMatchesRepository {
  constructor(private csvReader: FootballMatchCsvReader) {
    this.inti();
  }

  private inti(): void {}

  public getAll(): FootballMatch[] {
    return this.csvReader.readSync();
  }
}
