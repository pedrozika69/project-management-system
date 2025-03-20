<script>
  import { goto } from "$app/navigation";
  import { authStore } from "$lib/stores/authStore";
  import { onMount } from "svelte";
  import { get } from "svelte/store";
  
  const isAuthenticated = get(authStore).isAuthenticated;

  onMount(() => {
    if (!isAuthenticated) {
      goto("/auth/login", { replaceState: true });
    }
  });
</script>

<slot />