// Приклади роботи з колекціями в TypeScript з використанням flatMap
// flatMap працює як bind: перетворює кожен елемент на масив і "розплющує" результат

const numbers: number[] = [1, 2, 3, 4, 5];

// Приклад 1: Отримати всі пари (x, y) де x < y
const pairsExample = (): void => {
  const pairs = numbers.flatMap((x) =>
    numbers.filter((y) => x < y).map((y) => [x, y] as const),
  );

  console.log(
    `Пари (x, y) де x < y: ${pairs.map(([x, y]) => `(${x},${y})`).join(", ")}`,
  );
};

// Приклад 2: Генерація комбінацій
const colors: string[] = ["червоний", "зелений", "синій"];
const sizes: string[] = ["S", "M", "L"];

const combinationsExample = (): void => {
  const combinations = colors.flatMap((color) =>
    sizes.map((size) => `${color} ${size}`),
  );

  console.log(`Комбінації: ${combinations.join(", ")}`);
};

// Приклад 3: Фільтрація та трансформація вкладених колекцій
const nestedLists: number[][] = [[1, 2], [3, 4, 5], [6]];

const flattenAndFilterExample = (): void => {
  const result = nestedLists.flatMap((innerList) =>
    innerList.filter((x) => x % 2 === 0).map((x) => x * 10),
  );

  console.log(`Відфільтровані та помножені парні числа: ${result.join(", ")}`);
};

// Приклад 4: Ієрархічна структура (дерево)
type Tree<T> =
  | { kind: "leaf"; value: T }
  | { kind: "node"; children: Tree<T>[] };

const tree: Tree<number> = {
  kind: "node",
  children: [
    { kind: "leaf", value: 1 },
    {
      kind: "node",
      children: [
        { kind: "leaf", value: 2 },
        { kind: "leaf", value: 3 },
      ],
    },
    { kind: "leaf", value: 4 },
  ],
};

const flattenTree = <T>(tree: Tree<T>): T[] => {
  if (tree.kind === "leaf") {
    return [tree.value];
  } else {
    return tree.children.flatMap(flattenTree);
  }
};

const treeFlattenExample = (): void => {
  const flattened = flattenTree(tree);
  console.log(`Розплющене дерево: ${flattened.join(", ")}`);
};

// Приклад 5: Асинхронна обробка колекцій
const asyncCollectionsExample = async (): Promise<void> => {
  const asyncResults = await Promise.all(
    numbers.map(async (x) => {
      await new Promise((resolve) => setTimeout(resolve, 10)); // Симуляція асинхронної роботи
      // Повертаємо масив факторів числа
      const factors: number[] = [];
      for (let i = 1; i <= x; i++) {
        if (x % i === 0) factors.push(i);
      }
      return factors;
    }),
  );

  const flattened = asyncResults.flatMap((result) => result);

  console.log(`Асинхронні фактори: ${flattened.join(", ")}`);
};

export const runCollectionsExamples = async (): Promise<void> => {
  pairsExample();
  combinationsExample();
  flattenAndFilterExample();
  treeFlattenExample();
  await asyncCollectionsExample();
};
