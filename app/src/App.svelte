<script lang="ts">
  import { Router, Route } from 'svelte-routing';
  const Home = () => import('./pages/Home.svelte');
</script>

<Router>
    <Route path="/"  component={Home} exact />
</Router>