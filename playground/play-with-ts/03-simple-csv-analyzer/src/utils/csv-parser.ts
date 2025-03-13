export class CsvParser {
  static parse(buffer: string, delimiter: string = ','): string[][] {
    return buffer
      .split('\n')
      .filter(rowStr => rowStr.trim() != '')
      .map(rowStr => rowStr.split(delimiter).map(cellStr => cellStr.trim()));
  }

  static parseWithHeaders(
    buffer: string,
    delimiter: string = ','
  ): { headers: string[]; data: string[][] } {
    const parsedFile = CsvParser.parse(buffer, delimiter);
    return { headers: parsedFile.shift() || [], data: parsedFile };
  }
}
