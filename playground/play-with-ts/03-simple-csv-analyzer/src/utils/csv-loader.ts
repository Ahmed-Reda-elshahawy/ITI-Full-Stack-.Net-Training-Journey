import fs from 'fs';

export class CsvLoader {
  static loadSync(path: string): string {
    try {
      return fs.readFileSync(path, { encoding: 'utf-8' });
    } catch (error) {
      throw new Error(`Failed to load CSV file: ${(error as Error).message}`);
    }
  }
}
