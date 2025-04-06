export interface ServiceType {
  id: string;
  name: string;
  description: string;
  price: number;
  sessions: number;
}

export interface ServiceTypeDetail extends ServiceType {}
