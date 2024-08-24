import { BaseResponse, EResponse } from "./BaseResponse";

export class LinkZipResponse extends BaseResponse {
  public urls: string[] = [];

  public constructor(urls?: string[]) {
    super(EResponse.LinkZip);

    if (urls) {
      this.urls = urls;
    }
  }

  public override Initialize(data: any) {
    this.urls = Array.from(data.urls);
  }
}
