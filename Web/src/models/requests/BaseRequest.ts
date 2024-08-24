export enum ERequest {
  Unknown,
  Common,
  Rank,
  Currency,
  LinkZip,
  Radix,
  Temperature,
}

export class BaseRequest {
  public from: string = "";
  public to: string = "";

  constructor(from?: string, to?: string) {
    if (from) {
      this.from = from;
    }

    if (to) {
      this.to = to;
    }
  }

  // eslint-disable-next-line @typescript-eslint/no-unused-vars
  public Initialize(_: any) {
    throw new Error("Not implemented!");
  }
}
