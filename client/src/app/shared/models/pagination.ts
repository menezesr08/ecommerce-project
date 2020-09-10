import { IProduct } from './product';
// This interface returns the list of products and the pagination details from the api
export interface IPagination {
    pageIndex: number;
    pageSize: number;
    count: number;
    data: IProduct[];
  }
