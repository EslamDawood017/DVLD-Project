// Define the TypeScript interface for the query result
export interface ApplicationInfo {
    applicationID: number;
    applicationDate: Date;
    lastStatusDate: Date;
    paidFees: number;
    applicationTypeTitle: string;
    userName: string;
    status: 'New' | 'Cancelled' | 'Completed'; // Strictly typed status
  }
  