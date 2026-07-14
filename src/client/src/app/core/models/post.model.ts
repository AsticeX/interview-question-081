import { Comment } from './comment.model';

export interface Post {
  id: number;
  title: string;
  authorName: string;
  authorAvatarUrl: string;
  createdAt: string;
  imageUrl: string;
  comments: Comment[];
}
