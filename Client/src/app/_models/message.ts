export interface Message  {
  id: number;
  senderId: number;
  senderUsername: string;
  senderPhotoUrl: string;
  re3cipientId: number;
  recipientUsername: string;
  recipientPhotoUrl: string;
  content: string;
  dateRead?: Date;
  messageSent: string;
}