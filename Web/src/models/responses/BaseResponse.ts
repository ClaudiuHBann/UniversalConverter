export enum EResponse {
  Unknown,
  Common,
  Currency,
  Error,
  FromTo,
  LinkZip,
  Radix,
  Temperature,
}

export class BaseResponse {
  public type: EResponse;

  public constructor(type: EResponse) {
    this.type = type;
  }

  public Initialize(data: any) {
    throw new Error("Not implemented!");
  }
}
