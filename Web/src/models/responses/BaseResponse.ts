export enum EResponse {
  Unknown,
  Common,
  Rank,
  Currency,
  Error,
  FromTo,
  LinkZip,
  Radix,
  Temperature,
}

export class BaseResponse {
  public type: EResponse;

  protected constructor(type: EResponse) {
    this.type = type;
  }

  public Initialize(_: any) {
    throw new Error("Not implemented!");
  }
}
