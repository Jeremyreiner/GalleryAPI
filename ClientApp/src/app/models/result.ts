export class Result<T> {
    status: number;
    error: Error;
    value: T;


    constructor(value: T) {
        this.value = value;
        this.status = 200; 
        this.error = Error.None;
    }

    static Success<T>(value: T): Result<T> {
        return new Result<T>(value);
    }
}

export class Error {
    code: number;
  
    static None: Error = new Error(200);
  
    constructor(code: number) {
      this.code = code;
    }
  }