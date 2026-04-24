// Discriminated union in TypeScript.
// Це сукупність типів, де кожне значення має тег, який визначає різновид.

type Circle = { kind: "circle"; radius: number };
type Rectangle = { kind: "rectangle"; width: number; height: number };
type Triangle = { kind: "triangle"; baseLength: number; height: number };

type Shape = Circle | Rectangle | Triangle;

function area(shape: Shape): number {
  switch (shape.kind) {
    case "circle":
      return Math.PI * shape.radius * shape.radius;
    case "rectangle":
      return shape.width * shape.height;
    case "triangle":
      return (shape.baseLength * shape.height) / 2;
    default:
      const _exhaustiveCheck: never = shape;
      return _exhaustiveCheck;
  }
}

function describe(shape: Shape): string {
  switch (shape.kind) {
    case "circle":
      return `Circle with radius ${shape.radius}`;
    case "rectangle":
      return `Rectangle ${shape.width}x${shape.height}`;
    case "triangle":
      return `Triangle base ${shape.baseLength} height ${shape.height}`;
    default:
      const _exhaustiveCheck: never = shape;
      return _exhaustiveCheck;
  }
}

const shapes: Shape[] = [
  { kind: "circle", radius: 3 },
  { kind: "rectangle", width: 4, height: 5 },
  { kind: "triangle", baseLength: 6, height: 4 },
];

for (const shape of shapes) {
  console.log(`${describe(shape)} -> area = ${area(shape).toFixed(2)}`);
}
