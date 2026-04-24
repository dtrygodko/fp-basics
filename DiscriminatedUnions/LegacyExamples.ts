// Alternative TypeScript style using manual type guards and if/else branching.
// Це приклад до сучасного патерн-матчінгу, коли в коді вручну перевіряють `kind`.

type Circle = { kind: "circle"; radius: number };
type Rectangle = { kind: "rectangle"; width: number; height: number };
type Triangle = { kind: "triangle"; baseLength: number; height: number };

type Shape = Circle | Rectangle | Triangle;

function isCircle(shape: Shape): shape is Circle {
  return shape.kind === "circle";
}

function isRectangle(shape: Shape): shape is Rectangle {
  return shape.kind === "rectangle";
}

function isTriangle(shape: Shape): shape is Triangle {
  return shape.kind === "triangle";
}

function area(shape: Shape): number {
  if (isCircle(shape)) {
    return Math.PI * shape.radius * shape.radius;
  }

  if (isRectangle(shape)) {
    return shape.width * shape.height;
  }

  if (isTriangle(shape)) {
    return (shape.baseLength * shape.height) / 2;
  }

  throw new Error("Unknown shape");
}

function describe(shape: Shape): string {
  if (isCircle(shape)) {
    return `Circle with radius ${shape.radius}`;
  }

  if (isRectangle(shape)) {
    return `Rectangle ${shape.width}x${shape.height}`;
  }

  if (isTriangle(shape)) {
    return `Triangle base ${shape.baseLength} height ${shape.height}`;
  }

  throw new Error("Unknown shape");
}

const shapes: Shape[] = [
  { kind: "circle", radius: 3 },
  { kind: "rectangle", width: 4, height: 5 },
  { kind: "triangle", baseLength: 6, height: 4 },
];

for (const shape of shapes) {
  console.log(`${describe(shape)} -> area = ${area(shape).toFixed(2)}`);
}
