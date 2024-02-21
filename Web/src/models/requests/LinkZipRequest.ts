import { BaseRequest } from "./BaseRequest";

export class LinkZipRequest extends BaseRequest {
  public urls: string[] = [];

  public constructor(from: string, to: string, urls?: string[]) {
    super(from, to);

    if (urls) {
      this.urls = urls;
    }
  }

  public override Initialize(data?: any) {
    if (!data) {
      throw new Error("The urls are null!");
    }

    if (typeof data !== typeof this.urls) {
      throw new Error(`The data is not of type ${typeof this.urls}!`);
    }

    this.urls = data;
  }
}
