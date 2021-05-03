export interface Poll {
  id?: number;
  title: string;
  description: string;
  beginsAt: string;
  endsAt: string;
  options: Array<Poll>;
}
