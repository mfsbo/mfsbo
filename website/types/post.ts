export interface Post {
  url: string;
  title: string;
  date: Date;
  description: string;
  category: string;
  tags: string[];
  year: number;
  month: number;
  displayDate: string;
}
