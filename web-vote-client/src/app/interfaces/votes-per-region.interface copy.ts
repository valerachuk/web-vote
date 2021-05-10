export interface VotesPerRegion {
  id: number;
  name: string;

  votesCount: number;
  citizensCount: number;

  votesPercent: number | string;
  votersActivityPercent: number | string;
}
