export enum StateLifeCycle {
  Started = ('STARTED') as any,
  Success = ('SUCCESS') as any,
  Error = ('ERROR') as any,
  End = ('END') as any
}

let generatedID = 0;
export const GenerateId = () => generatedID++;
