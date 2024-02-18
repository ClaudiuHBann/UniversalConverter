class LinkZipResponse extends BaseResponse {
  urls: string[] = [];

  constructor(urls: string[]) {
    super();
    this.urls = urls;
  }
}
