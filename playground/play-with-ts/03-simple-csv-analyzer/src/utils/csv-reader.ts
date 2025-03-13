import { CsvLoader } from './csv-loader';
import { CsvParser } from './csv-parser';

export abstract class CsvReader<T> {
  constructor(
    private csvFilePath: string,
    private delimiter: string = ','
  ) {}

  private readFileSync(): string[][] {
    return CsvParser.parse(
      CsvLoader.loadSync(this.csvFilePath),
      this.delimiter
    );
  }

  private readFileWithHeadersSync(): { headers: string[]; data: string[][] } {
    return CsvParser.parseWithHeaders(
      CsvLoader.loadSync(this.csvFilePath),
      this.delimiter
    );
  }

  protected abstract map(dataRow: string[]): T;

  readSync(): T[] {
    return this.readFileSync().map(dataRow => this.map(dataRow));
  }

  readWithHeadersSync(): { headers: string[]; data: T[] } {
    const { headers, data } = this.readFileWithHeadersSync();

    return { headers, data: data.map(dataRow => this.map(dataRow)) };
  }
}
