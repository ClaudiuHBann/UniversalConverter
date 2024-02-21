import { BaseRequest } from "./BaseRequest";

export class CommonRequest extends BaseRequest {
  public override Initialize(data?: any) {
    if (data) {
      throw new Error("The data should be null!");
    }
  }
}
