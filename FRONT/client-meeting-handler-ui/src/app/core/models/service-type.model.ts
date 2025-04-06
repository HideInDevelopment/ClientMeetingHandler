export interface ServiceType {
  Id: string;
  Name: string;
  Description: string;
  Price: number;
  Sessions: number;
}

export interface ServiceTypeDetail extends ServiceType {}
