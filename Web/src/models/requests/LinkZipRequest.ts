class LinkZipRequest extends BaseRequest {
  urls: string[] = [];

  constructor(from: string, to: string, urls: string[]) {
    super(from, to);
    this.urls = urls;
  }
}
