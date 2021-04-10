export interface Poll {
  id?: number;
  title: string;
  description: string;
  options: Array<Poll>;
}
