import { IMappable } from '../interfaces/IMappable';
import { Location } from './location';
import { faker } from '@faker-js/faker';

export class User implements IMappable {
  name: string;
  age: number;
  location: Location;

  constructor() {
    this.name = faker.name.fullName();
    this.age = faker.number.int({ min: 20, max: 55 });
    this.location = new Location();
  }

  markerContent(): string {
    return `User: (name: ${this.name})`;
  }
}
