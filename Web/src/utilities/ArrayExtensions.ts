export function FindIndex(
  array: string[],
  item: string,
  icase: boolean = true
): number {
  if (icase) {
    item = item.toLowerCase();
  }

  return array.findIndex(
    (value) => (icase ? value.toLowerCase() : value) === item
  );
}

export function FindItem(
  array: string[],
  item: string,
  icase: boolean = true
): string | null {
  const index = FindIndex(array, item, icase);
  return index === -1 ? null : array[index];
}

export function Contains(
  array: string[],
  item: string,
  icase: boolean = true
): boolean {
  return FindIndex(array, item, icase) !== -1;
}

export function SplitByAnySpace(str: string): string[] {
  return str.trim().split(/\s+/);
}

export function SplitByAnySpaceAndComma(str: string): string[] {
  return str.trim().split(/[\s,]+/);
}
