// Pattern matching у TypeScript через дискриміновані об'єднання та перевірки типів.

type Circle = { kind: "circle"; radius: number };
type Rectangle = { kind: "rectangle"; width: number; height: number };
type Triangle = { kind: "triangle"; baseLength: number; height: number };
type Point = { kind: "point" };

type Shape = Circle | Rectangle | Triangle | Point;

function area(shape: Shape): number {
  switch (shape.kind) {
    case "circle":
      if (shape.radius <= 0) throw new Error("Некоректні параметри форми");
      return Math.PI * shape.radius * shape.radius;
    case "rectangle":
      if (shape.width <= 0 || shape.height <= 0) throw new Error("Некоректні параметри форми");
      return shape.width * shape.height;
    case "triangle":
      if (shape.baseLength <= 0 || shape.height <= 0) throw new Error("Некоректні параметри форми");
      return (shape.baseLength * shape.height) / 2;
    case "point":
      return 0;
    default:
      const _exhaustiveCheck: never = shape;
      return _exhaustiveCheck;
  }
}

function describe(shape: Shape): string {
  switch (shape.kind) {
    case "circle":
      return `Коло з радіусом ${shape.radius.toFixed(2)}`;
    case "rectangle":
      return `Прямокутник ${shape.width.toFixed(2)}x${shape.height.toFixed(2)}`;
    case "triangle":
      return `Трикутник з основою ${shape.baseLength.toFixed(2)} та висотою ${shape.height.toFixed(2)}`;
    case "point":
      return "Точка";
    default:
      const _exhaustiveCheck: never = shape;
      return _exhaustiveCheck;
  }
}

function classifyNumber(value: number): string {
  if (value === 0) return "Нуль";
  if (value === 1) return "Одиниця";
  if ([2, 3, 5, 7, 11].includes(value)) return "Мале просте число";
  if (value < 0) return "Від'ємне число";
  if (value % 2 === 0) return "Чітне число";
  return "Непарне число";
}

function describePair(pair: [number, number]): string {
  const [x, y] = pair;
  if (x === 0 && y === 0) return "Початок координат";
  if (y === 0) return `На осі X: ${x}`;
  if (x === 0) return `На осі Y: ${y}`;
  return `Точка (${x}, ${y})`;
}

function printOption(value: number | null): string {
  if (value === null) return "Немає значення";
  if (value > 10) return `Велике значення ${value}`;
  return `Мале або середнє значення ${value}`;
}

const shapes: Shape[] = [
  { kind: "circle", radius: 3 },
  { kind: "rectangle", width: 4, height: 5 },
  { kind: "triangle", baseLength: 6, height: 4 },
  { kind: "point" },
];

for (const shape of shapes) {
  console.log(`${describe(shape)} -> площа = ${area(shape).toFixed(2)}`);
}

for (const value of [-1, 0, 2, 4, 13]) {
  console.log(`${value} -> ${classifyNumber(value)}`);
}

for (const pair of [
  [0, 0],
  [5, 0],
  [0, 3],
  [2, 3],
] as [number, number][]) {
  console.log(describePair(pair));
}

const options: (number | null)[] = [5, 20, null];
for (const o of options) {
  console.log(printOption(o));
}
