export function ToLowerCaseAndCapitalize(str: string) {
  str = str.toLowerCase();
  return str[0].toUpperCase() + str.slice(1);
}
