import { BaseRequest } from "./BaseRequest";

export class LinkZipRequest extends BaseRequest {
  public urls: string[] = [];

  public constructor(from: string, to: string, urls?: string[]) {
    super(from, to);

    if (urls) {
      this.urls = urls;
    }
  }

  public override Initialize(data: string[]) {
    if (!data) {
      throw new Error("The urls are null!");
    }

    this.urls = data;
  }
}
