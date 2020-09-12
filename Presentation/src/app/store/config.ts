import { isE2E } from '../core/e2e-check';

// General purpose DataService stuff, good for any entity type
export const api = '/api';
export const fakeDelays = isE2E
  ? { select: 0, save: 0 }
  : { select: 1000, save: 200 };

export class DataServiceError<T> {
  constructor(public error: any, public requestData: T) {}
}
