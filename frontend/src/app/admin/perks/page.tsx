"use client";

import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import { z } from "zod";
import { Button } from "@/components/ui/button";
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/components/ui/form";
import { Input } from "@/components/ui/input";
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import { toast } from "sonner";
import { useState, useEffect, useCallback } from "react";
import { ButtonLoading } from "@/components/button-loading";
import type Perk from "@/models/Perk";
import { ScrollArea } from "@/components/ui/scroll-area";

// Esquema Zod simplificado, pois o ID é gerenciado pelo estado, não pelo formulário.
const formSchema = z.object({
  name: z.string().min(2, "Name must be at least 2 characters.").max(50),
  description: z.string().min(5, "Description must be at least 5 characters."),
});

const API_URL = process.env.NEXT_PUBLIC_API_URL || "http://localhost:5091/api";

export default function PerksPage() {
  const [isLoading, setIsLoading] = useState(false);
  const [perks, setPerks] = useState<Perk[]>([]);
  const [selectedPerk, setSelectedPerk] = useState<Perk | null>(null);

  useEffect(() => {
    fetchPerks();
  }, []);

  const form = useForm<z.infer<typeof formSchema>>({
    resolver: zodResolver(formSchema),
    defaultValues: {
      name: "",
      description: "",
    },
  });

  const fetchPerks = async () => {
    try {
      const response = await fetch(`${API_URL}/Perk`);
      if (!response.ok) {
        throw new Error("Failed to fetch perks");
      }
      const data: Perk[] = await response.json();
      setPerks(data);
    } catch (error) {
      console.error(error);
      toast.error("Could not load perks.");
    }
  };

  const handleClearForm = useCallback(() => {
    form.reset({
      name: "",
      description: "",
    });
    setSelectedPerk(null);
  }, [form]);

  async function onSubmit(values: z.infer<typeof formSchema>) {
    setIsLoading(true);
    const isUpdating = selectedPerk !== null;
    const url = isUpdating ? `${API_URL}/Perk/${selectedPerk.id}` : `${API_URL}/Perk`;
    const method = isUpdating ? "PUT" : "POST";
    const successMessage = `Perk ${isUpdating ? "updated" : "created"} successfully!`;
    const errorMessage = `Failed to ${isUpdating ? "update" : "create"} perk.`;

      const payload = isUpdating
    ? { ...values, id: selectedPerk.id } // Para atualizar, enviamos os dados do form + o ID.
    : values; // Para criar, enviamos apenas os dados do form.

    try {
      const response = await fetch(url, {
        method: method,
        body: JSON.stringify(payload),
        headers: {
          "Content-Type": "application/json",
        },
      });

      if (!response.ok) throw new Error("Server responded with an error");

      toast.success(successMessage);
      handleClearForm();
      await fetchPerks();
    } catch (error) {
      toast.error(errorMessage);
      console.error(error);
    } finally {
      setIsLoading(false);
    }
  }
  
  // 1. Nova função para lidar com a exclusão do perk
  const handleDeletePerk = async () => {
    if (!selectedPerk) return;

    // 2. Adiciona uma confirmação para evitar exclusões acidentais
    const isConfirmed = window.confirm(`Are you sure you want to delete the perk "${selectedPerk.name}"?`);
    if (!isConfirmed) return;

    setIsLoading(true);
    try {
      const response = await fetch(`${API_URL}/Perk/${selectedPerk.id}`, {
        method: "DELETE",
      });

      if (!response.ok) {
        throw new Error("Failed to delete perk");
      }
      
      toast.success("Perk deleted successfully!");
      handleClearForm();
      await fetchPerks();
    } catch (error) {
      toast.error("Failed to delete perk.");
      console.error(error);
    } finally {
      setIsLoading(false);
    }
  };

  const handlePerkClick = useCallback((perk: Perk) => {
    setSelectedPerk(perk);
    form.reset({
      name: perk.name,
      description: perk.description,
    });
  }, [form]);

  return (
    <main className="w-full h-dvh flex items-stretch justify-center gap-8 p-40">
      <Card className="flex-1">
        <CardHeader>
          <CardTitle>{selectedPerk ? "Edit Perk" : "Create a New Perk"}</CardTitle>
          <CardDescription>
            {selectedPerk ? `Editing "${selectedPerk.name}"` : "Fill out the form to create a new perk."}
          </CardDescription>
        </CardHeader>
        <CardContent>
          <Form {...form}>
            <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-8">
              <FormField
                control={form.control}
                name="name"
                render={({ field }) => (
                  <FormItem>
                    <FormLabel>Perk Name</FormLabel>
                    <FormControl>
                      <Input placeholder="E.g., Number of Accounts" {...field} />
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />
              <FormField
                control={form.control}
                name="description"
                render={({ field }) => (
                  <FormItem>
                    <FormLabel>Perk Description</FormLabel>
                    <FormControl>
                      <Input placeholder="E.g., Number of users allowed" {...field} />
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />
              <div className="flex items-center gap-2 flex-wrap">
                {isLoading ? (
                  <ButtonLoading />
                ) : (
                  <>
                    <Button type="submit" disabled={isLoading}>
                      {selectedPerk ? 'Update Perk' : 'Create Perk'}
                    </Button>
                    {/* 3. Botão de exclusão renderizado condicionalmente */}
                    {selectedPerk && (
                      <Button
                        type="button"
                        variant="destructive"
                        onClick={handleDeletePerk}
                        disabled={isLoading}
                      >
                        Delete Perk
                      </Button>
                    )}
                  </>
                )}
                <Button type="button" variant="secondary" onClick={handleClearForm}>
                  Clear
                </Button>
              </div>
            </form>
          </Form>
        </CardContent>
      </Card>

      <Card className="flex-1">
        <CardHeader>
          <CardTitle>Available Perks</CardTitle>
          <CardDescription>Click a perk to edit it.</CardDescription>
        </CardHeader>
        <CardContent>
          <ScrollArea className="w-full h-80">
            {perks.length === 0 ? (
              <p>No perks available.</p>
            ) : (
              <div className="space-y-2">
                {perks.map((perk) => (
                  <Button
                    key={perk.id}
                    variant={selectedPerk?.id === perk.id ? "default" : "outline"}
                    className="w-full h-auto text-left flex flex-col items-start p-4"
                    onClick={() => handlePerkClick(perk)}
                  >
                    <h3 className="font-bold">{perk.name}</h3>
                    <p className="text-sm text-muted-foreground">{perk.description}</p>
                  </Button>
                ))}
              </div>
            )}
          </ScrollArea>
        </CardContent>
      </Card>
    </main>
  );
}