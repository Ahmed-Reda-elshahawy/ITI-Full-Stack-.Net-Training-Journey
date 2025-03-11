import { faker } from '@faker-js/faker';
import { Location } from './location';
import { IMappable } from '../interfaces/IMappable';

export class Company implements IMappable{
  name: string;
  catchPhrase: string;
  location: Location;

  constructor() {
    this.name = faker.company.name();
    this.catchPhrase = faker.company.catchPhrase();
    this.location = new Location();
  }

  markerContent(): string {
    return `Company: (name: ${this.name})`;
  }
}
