import { BaseResponse, EResponse } from "./BaseResponse";

export class ErrorResponse extends BaseResponse {
  public code: number = 0;
  public message: string = "";

  public constructor(code?: number, message?: string) {
    super(EResponse.Error);

    if (code && message) {
      this.code = code;
      this.message = message;
    }
  }

  public override Initialize(data: any) {
    this.code = data.code!;
    this.message = data.message!;
  }
}
