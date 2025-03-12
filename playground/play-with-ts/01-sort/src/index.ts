import { Sorter } from './Sorter';

let arr = [10, -3, 2, -5, 0];
console.log(arr);
console.log(new Sorter(arr).sort());
console.log(arr);

console.log(new Sorter('bcad').sort());
