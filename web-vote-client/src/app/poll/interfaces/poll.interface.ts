export interface Poll {
  title: string;
  description: string;
  options: Array<Poll>;
}
