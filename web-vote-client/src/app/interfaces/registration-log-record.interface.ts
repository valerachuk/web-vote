export interface RegistrationLogRecord {
  id: number;
  timestamp: string;

  byWhomId: number;
  byWhomName: string;
  byWhomITN: string;

  toWhomId: number;
  toWhomName: string;
  toWhomITN: string;
}
