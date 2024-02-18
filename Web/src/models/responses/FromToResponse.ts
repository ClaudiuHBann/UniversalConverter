class FromToResponse extends BaseResponse {
  fromTo: string[] = [];

  constructor(fromTo: string[]) {
    super();
    this.fromTo = fromTo;
  }
}
