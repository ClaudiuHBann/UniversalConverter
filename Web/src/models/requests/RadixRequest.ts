import { BaseRequest } from "./BaseRequest";

export class RadixRequest extends BaseRequest {
  public numbers: string[] = [];

  public constructor(from: string, to: string, numbers: string[]) {
    super(from, to);
    this.numbers = numbers;
  }
}
