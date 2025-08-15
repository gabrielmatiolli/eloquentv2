import SubscriptionPerk from "./SubscriptionPerk";

export default interface Subscription {
  id: number;
  name: string;
  description: string;
  price: number;
  subscriptionPerks: SubscriptionPerk[];
}
