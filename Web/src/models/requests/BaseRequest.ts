export enum ERequest {
  Unknown,
  Common,
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

  public Initialize(data?: any) {
    throw new Error("Not implemented!");
  }
}
