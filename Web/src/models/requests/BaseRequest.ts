export class BaseRequest {
  public from: string = "";
  public to: string = "";

  public constructor(from: string, to: string) {
    this.from = from;
    this.to = to;
  }
}
