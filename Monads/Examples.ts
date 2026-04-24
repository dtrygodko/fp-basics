// TypeScript: прості аналоги монадам у F#
// - Maybe/Option: union type з chain
// - Result: успіх/помилка
// - Async: Promise
// - Railway Programming: комбінація Result, Maybe та Async
// TypeScript не підтримує створення власних операторів, тому використовуємо функції.

type Maybe<T> = { kind: "just"; value: T } | { kind: "nothing" };

const just = <T>(value: T): Maybe<T> => ({ kind: "just", value });
const nothing = <T>(): Maybe<T> => ({ kind: "nothing" });

const maybeBind = <T, U>(m: Maybe<T>, fn: (value: T) => Maybe<U>): Maybe<U> =>
  m.kind === "just" ? fn(m.value) : nothing();

const maybeMap = <T, U>(m: Maybe<T>, fn: (value: T) => U): Maybe<U> =>
  m.kind === "just" ? just(fn(m.value)) : nothing();

const tryDivide = (x: number, y: number): Maybe<number> =>
  y === 0 ? nothing() : just(Math.floor(x / y));

const optionExample = (): string => {
  const result = maybeBind(just(10), (x) => tryDivide(x, 2));
  const final = maybeBind(result, (x) => tryDivide(x, 5));

  return final.kind === "just"
    ? `Результат option: ${final.value}`
    : "Неможливо поділити";
};

type Result<T, E> = { kind: "ok"; value: T } | { kind: "error"; error: E };

const ok = <T, E>(value: T): Result<T, E> => ({ kind: "ok", value });
const error = <T, E>(err: E): Result<T, E> => ({ kind: "error", error: err });

const resultBind = <T, U, E>(
  r: Result<T, E>,
  fn: (value: T) => Result<U, E>,
): Result<U, E> => (r.kind === "ok" ? fn(r.value) : error(r.error));

const resultMap = <T, U, E>(
  r: Result<T, E>,
  fn: (value: T) => U,
): Result<U, E> => (r.kind === "ok" ? ok(fn(r.value)) : error(r.error));

const parsePositiveInt = (text: string): Result<number, string> => {
  const value = Number(text);
  return Number.isInteger(value) && value > 0
    ? ok(value)
    : error("Число має бути додатнім або не вдалося розпарсити");
};

const resultExample = (): string => {
  const computation = resultBind(parsePositiveInt("12"), (x) =>
    resultMap(parsePositiveInt(String(x - 2)), (y) => y * 3),
  );

  return computation.kind === "ok"
    ? `Результат Result: ${computation.value}`
    : `Помилка Result: ${computation.error}`;
};

const asyncWork = async (x: number): Promise<number> => {
  await new Promise((resolve) => setTimeout(resolve, 100));
  return x * x;
};

const asyncExample = async (): Promise<string> => {
  const a = await asyncWork(3);
  const b = await asyncWork(4);
  return `Результат async: ${a + b}`;
};

const asyncOptionExample = async (): Promise<string> => {
  const result = await asyncMaybeBind(asyncTryDivide(10, 2), (x) =>
    asyncTryDivide(x, 5),
  );

  return result.kind === "just"
    ? `Результат AsyncOption: ${result.value}`
    : "Неможливо поділити асинхронно";
};

// Практичний AsyncOption: Promise<Maybe<T>>
type AsyncMaybe<T> = Promise<Maybe<T>>;

const asyncTryDivide = (x: number, y: number): AsyncMaybe<number> =>
  Promise.resolve(y === 0 ? nothing() : just(Math.floor(x / y)));

const asyncMaybeBind = async <T, U>(
  m: AsyncMaybe<T>,
  fn: (value: T) => AsyncMaybe<U>,
): Promise<Maybe<U>> => {
  const maybe = await m;
  return maybe.kind === "just" ? fn(maybe.value) : nothing();
};

const asyncMaybeMap = async <T, U>(
  m: AsyncMaybe<T>,
  fn: (value: T) => U,
): Promise<Maybe<U>> => {
  const maybe = await m;
  return maybeMap(maybe, fn);
};

const divisionIfEven = (x: number): Maybe<number> =>
  x % 2 === 0 ? just(Math.floor(x / 2)) : nothing();

const maybeToResult = <T, E>(m: Maybe<T>, err: E): Result<T, E> =>
  m.kind === "just" ? ok(m.value) : error(err);

const railwayExample = (): string => {
  const computation = resultMap(
    resultBind(parsePositiveInt("20"), (x) =>
      maybeToResult(divisionIfEven(x), "Число не парне"),
    ),
    (x) => x + 5,
  );

  return computation.kind === "ok"
    ? `Результат Railway: ${computation.value}`
    : `Помилка Railway: ${computation.error}`;
};

const parsePositiveAsync = async (text: string): Promise<Maybe<number>> => {
  const result = parsePositiveInt(text);
  return result.kind === "ok" ? just(result.value) : nothing();
};

const railwayAsyncExample = async (): Promise<string> => {
  const result = await asyncMaybeMap(
    asyncMaybeBind(parsePositiveAsync("20"), (x) => asyncTryDivide(x, 2)),
    (x) => x + 1,
  );

  return result.kind === "just"
    ? `Результат Railway AsyncOption: ${result.value}`
    : "Помилка Railway AsyncOption";
};

export const runExamples = async (): Promise<void> => {
  console.log(optionExample());
  console.log(resultExample());
  console.log(await asyncExample());
  console.log(await asyncOptionExample());
  console.log(railwayExample());
  console.log(await railwayAsyncExample());
};
