class Examples {
  static main() {
    let x = 5;

    let list = [1, 2, 3, 4, 5];

    const doubled =
      list
      .filter((x) => x > 1)
      .map((x) => x * 2);

    let name: string | null = null;

    if (name !== null) {
      console.log(name.length);
    }

    const point: [number, number] = [3, 4];
    const [xCoord, yCoord] = point;
    console.log(`Point: ${xCoord}, ${yCoord}`);

    const swapped: [number, number] = [yCoord, xCoord];
    console.log(`Swapped: ${swapped[0]}, ${swapped[1]}`);

    const pair: [string, number] = ["hello", 42];
    const [message, number] = pair;
    console.log(`Pair: ${message} = ${number}`);
  }

  add(a: number, b: number): number {
    return a + b;
  }
}

interface User {
  Name: string;
}
