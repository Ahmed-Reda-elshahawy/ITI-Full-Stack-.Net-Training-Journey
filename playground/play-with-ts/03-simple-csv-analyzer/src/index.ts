import path from 'path';
import { FootballMatchCsvReader } from './football-match/football-match-csv-reader';
import { FootballMatchesRepository } from './football-match/football-match.repository';

// initialize dependencies
const footballCSVPath = path.join(__dirname, '..', 'data', 'football.csv.data');
const footballMatchCsvReader = new FootballMatchCsvReader(footballCSVPath);
const footballMatchRepo = new FootballMatchesRepository(footballMatchCsvReader);

console.log(footballMatchRepo.getAll()[0]);
