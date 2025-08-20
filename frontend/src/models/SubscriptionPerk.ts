import Perk from "./Perk";

export default interface SubscriptionPerk {
  perkId: number;
  subscriptionId: number;
  value?: number | null;
  perk?: Perk;
}
