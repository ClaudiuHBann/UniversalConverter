import { BaseRequest } from "./BaseRequest";

export class LinkZipRequest extends BaseRequest {
  public urls: string[] = [];

  public constructor(from: string, to: string, urls: string[]) {
    super(from, to);
    this.urls = urls;
  }
}
