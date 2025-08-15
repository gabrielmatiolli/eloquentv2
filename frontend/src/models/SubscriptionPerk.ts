import Perk from "./Perk";

export default interface SubscriptionPerk {
  perkId: number;
  subscriptionId: number;
  numericValue?: number | null;
  booleanValue?: boolean | null;
  perk?: Perk;
}
