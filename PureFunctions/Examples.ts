export namespace PureFunctions {
  // Чиста функція: без змін глобального стану, без побічних ефектів.
  export function add(a: number, b: number): number {
    return a + b;
  }

  // Чиста функція, без мутації, без зовнішніх ресурсів.
  export function sumList(values: readonly number[]): number {
    return values.reduce((total, x) => total + x, 0);
  }

  // Функція, що може повернути null замість побічного ефекту або аварійного стану.
  export function tryParseInt(text: string): number | null {
    const num = Number(text);
    return Number.isInteger(num) ? num : null;
  }

  // Функція, яка може кинути виключення, хоча сигнатура лише повертає number.
  export function parsePositiveId(value: string): number {
    const id = Number(value);
    if (!Number.isInteger(id)) {
      throw new Error("Неправильний формат ID");
    }
    if (id <= 0) {
      throw new RangeError("ID має бути додатнім");
    }
    return id;
  }

  // Приклад чистої трансформації, без спільного стану.
  export function reverseString(text: string): string {
    return text.split("").reverse().join("");
  }

  // Функція, сигнатура якої не показує, що вона може повернути null.
  // У JavaScript/TypeScript це один з випадків прихованих побічних ефектів.
  export function getItemById(
    items: { id: string; name: string }[],
    id: string,
  ): { id: string; name: string } | null {
    const item = items.find((x) => x.id === id);
    return item === undefined ? null : item;
  }

  export function exampleUsage() {
    const total = add(3, 7);
    const sum = sumList([1, 2, 3]);
    const maybeValue = tryParseInt("42");
    const item = getItemById([{ id: "1", name: "Test" }], "1");
    const reversed = reverseString("Hello");
    const id = parsePositiveId("10");
    console.log({ total, sum, maybeValue, item, reversed, id });
  }
}
