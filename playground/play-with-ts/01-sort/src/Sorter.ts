type TComparator<T> = (left: T, right: T) => 1 | 0 | -1;

export class Sorter<T extends any[] | string> {
  protected bag: any[] = [];
  protected result?: T;
  private compare: TComparator<any>;

  constructor(
    private collection: T,
    comparator?: TComparator<any>
  ) {
    this.compare =
      comparator ??
      ((left, right) => (left > right ? 1 : left < right ? -1 : 0));
  }

  protected swap(leftIndex: number, rightIndex: number): void {
    [this.bag[leftIndex], this.bag[rightIndex]] = [
      this.bag[rightIndex],
      this.bag[leftIndex],
    ];
  }

  protected preSort(): void {
    this.bag =
      typeof this.collection === 'string'
        ? this.collection.split('')
        : [...this.collection];
  }

  protected postSort(): void {
    this.result =
      typeof this.collection === 'string'
        ? (this.bag.join('') as T)
        : (this.bag as T);
  }

  public sort(): T {
    if (this.result) return this.result;
    
    this.preSort();

    const { length } = this.bag;
    for (let i = 0; i < length; ++i) {
      for (let j = 0; j < length - i - 1; ++j) {
        if (this.compare(this.bag[j], this.bag[j + 1]) > 0) {
          this.swap(j, j + 1);
        }
      }
    }

    this.postSort();
    return this.result!;
  }
}
